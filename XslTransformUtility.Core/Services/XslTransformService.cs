using System;
using System.Xml.Xsl;

namespace DjK.XslTransformUtility.Core.Services
{
    public class XslTransformService
    {
        readonly IMessageWriter messageWriter;
        readonly IInputReader inputReader;
        readonly IAppConfigService appConfigService;

        public XslTransformService(IMessageWriter messageWriter, IInputReader inputReader, IAppConfigService appConfigService)
        {
            this.messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            this.inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            this.appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            Initialize();
            appConfigService.AppConfigChanged += OnAppConfigChanged;
        }

        private void Initialize()
        {
            appConfigService.LoadAppConfiguration();
        }

        public void OnAppConfigChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        public void Transform()
        {
            var xslt = new XslCompiledTransform();

            try
            {
                xslt.Load(appConfigService.XslFile);
            }
            catch (Exception e)
            {
                messageWriter.Write(e.ToString());
                inputReader.GetInput();
            }
            
            try
            {
                xslt.Transform(appConfigService.XmlFile, appConfigService.XmlFile + "_out");
                messageWriter.Write("Success!");
            }
            catch (Exception e)
            {
                messageWriter.Write(e.ToString());
                inputReader.GetInput();
            }
        }

    }
    
}