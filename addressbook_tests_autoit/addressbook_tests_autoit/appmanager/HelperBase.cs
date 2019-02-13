using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class HelperBase
    {
        protected string WINTITLE;

        protected ApplicationManager manager;
        protected AutoItX3 aux;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            WINTITLE = ApplicationManager.WINTITLE;
            aux = manager.Aux;
        }
    }
}