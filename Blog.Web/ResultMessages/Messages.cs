namespace Blog.Web.ResultMessages
{
    public static class Messages
    {
        public static class Article
        {
            public static string Add(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale eklendi.";
            }
            public static string Update(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale güncellendi.";
            }
            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale silindi.";
            }
        }
        public static class Category
        {
            public static string Add(string categoryName)
            {
                return $"{categoryName} başlıklı kategori eklendi.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} başlıklı kategori güncellendi.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} başlıklı kategori silindi.";
            }
        }
        public static class User
        {
            public static string Add(string userName)
            {
                return $"{userName} email adresli kullanıcı eklendi.";
            }
            public static string Update(string userName)
            {
                return $"{userName} email adresli kullanıcı güncellendi.";
            }
            public static string Delete(string userName)
            {
                return $"{userName} email adresli kullanıcı silindi.";
            }
        }
    }
}
