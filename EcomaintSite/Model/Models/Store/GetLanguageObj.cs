namespace Model.Data
{
    using System;

    public partial class GetLanguageObj
    {
        public int Index { get; set; }
        public string ModuleName { get; set; }
        public string Form { get; set; }
        public string Keyword { get; set; }
        public string VietNam { get; set; }
        public string English { get; set; }
        public string Chinese { get; set; }
        public string Article { get; set; }
        public bool Type { get; set; }
        public string VietNamOld { get; set; }
        public string EnglishOld { get; set; }
        public string ChineseOld { get; set; }
    }
}