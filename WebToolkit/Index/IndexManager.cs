using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections.Generic;

namespace WebToolkit.Index
{
    public class IndexManager
    {
        private string indexPath;
        private string IndexPath
        {
            get { return indexPath ?? (indexPath = IndexSettings.IndexPath); }
        }

        public bool IndexExists
        {
            get { return System.IO.File.Exists(IndexPath); }
        }

        public void Index(List<Field> fields)
        {
            var analyzer = new AccentedAnalyzer();
            var directory = FSDirectory.Open(System.IO.Directory.GetParent(IndexPath));
            var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.LIMITED);
            AddDocument(writer, fields);
            writer.Optimize();
            writer.Commit();
            writer.Close();
        }

        private void AddDocument(IndexWriter writer, List<Field> fields)
        {
            var document = new Document();
            foreach (Field field in fields)
            {
                document.Add(field); //new Field("key", key, Field.Store.YES, Field.Index.ANALYZED)
            }
            writer.AddDocument(document);
        }

        public void RemoveFromIndex(string[] keys, string value)
        {
            var analyzer = new AccentedAnalyzer();
            var query = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, keys, analyzer);
            var directory = FSDirectory.Open(System.IO.Directory.GetParent(IndexPath));
            IndexReader idxReader = IndexReader.Open(indexPath);
            var searcher = new IndexSearcher(directory, true);
            query.SetDefaultOperator(QueryParser.Operator.AND);
            var q = query.Parse(value);
            int top = idxReader.MaxDoc();
            var results = TopScoreDocCollector.create(top, true);
            searcher.Search(q, results);
            ScoreDoc[] hits = results.TopDocs().scoreDocs;
            Document[] documents = new Document[hits.Length];
            IndexReader indexReader = null;
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].doc;
                indexReader = IndexReader.Open(directory, false);
                indexReader.DeleteDocument(docId);
                indexReader.Commit();
                indexReader.Flush();
                indexReader.Close();
            }
            searcher.Close();
            directory.Close();
        }

        public Document[] Query(string[] keys, string value, out int resultCount)
        {
            var analyzer = new AccentedAnalyzer();
            var query = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, keys, analyzer);
            var directory = FSDirectory.Open(System.IO.Directory.GetParent(IndexPath));
            IndexReader idxReader = IndexReader.Open(indexPath);
            var searcher = new IndexSearcher(directory, true);
            query.SetDefaultOperator(QueryParser.Operator.AND);
            var q = query.Parse(value);
            int top = idxReader.MaxDoc();
            var results = TopScoreDocCollector.create(top, true);
            searcher.Search(q, results);
            ScoreDoc[] hits = results.TopDocs().scoreDocs;
            Document[] documents = new Document[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].doc;
                documents[i] = searcher.Doc(docId);
            }
            resultCount = hits.Length;
            searcher.Close();
            directory.Close();
            return documents;
        }     
    }
}
