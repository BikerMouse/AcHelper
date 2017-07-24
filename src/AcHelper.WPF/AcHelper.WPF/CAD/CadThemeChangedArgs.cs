namespace AcHelper.WPF.CAD
{
    public class CadThemeChangedArgs
    {
        private readonly string _theme;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        public CadThemeChangedArgs(string theme)
        {
            _theme = theme;
        }
        /// <summary>
        /// Name of the new theme.
        /// </summary>
        public string Theme => _theme;
    }
}