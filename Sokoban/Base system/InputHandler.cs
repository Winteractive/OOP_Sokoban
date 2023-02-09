/*
 _    _ _       _                      _   _              _________    _____  _____  _____  _____ 
| |  | (_)     | |                    | | (_)            / /  __ \ \  / __  \|  _  |/ __  \/ __  \
| |  | |_ _ __ | |_ ___ _ __ __ _  ___| |_ ___   _____  | || /  \/| | `' / /'| |/' |`' / /'`' / /'
| |/\| | | '_ \| __/ _ \ '__/ _` |/ __| __| \ \ / / _ \ | || |    | |   / /  |  /| |  / /    / /  
\  /\  / | | | | ||  __/ | | (_| | (__| |_| |\ V /  __/ | || \__/\| | ./ /___\ |_/ /./ /___./ /___
 \/  \/|_|_| |_|\__\___|_|  \__,_|\___|\__|_| \_/ \___| | | \____/| | \_____/ \___/ \_____/\_____/
                                                         \_\     /_/                                                                                                                               
*/


namespace EMMA_ENGINE.Input
{
    public abstract class InputHandler
    {
        private InputHandler next;

        public string RecieveInput(string input)
        {
            return SendInput(ManipulateInput(input));
        }

        protected abstract string ManipulateInput(string input);


        private string SendInput(string input)
        {
            return next == null ? input : next.RecieveInput(input);
        }

        public void SetNextHandler(InputHandler _next)
        {
            if (_next == this) return;
            next = _next;
        }

        public InputHandler GetNextHandler()
        {
            return next;
        }

        public InputHandler GetLastHandler()
        {
            return next == null ? this : next.GetLastHandler();
        }

    }
}