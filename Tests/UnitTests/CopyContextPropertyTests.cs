using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;

namespace BizTalkComponents.CopyContextProperty.Tests.UnitTests
{
    [TestClass]
    public class CopyContextPropertyTests
    {
        [TestMethod]
        public void TestCopyProperty()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            
            var component = new PipelineComponents.CopyContextProperty.CopyContextProperty
            {
                SourceProperty = "http://tempuri.org#Source",
                DestinationPropertyName = "http://tempuri.org#Destination"
            };

            pipeline.AddComponent(component,PipelineStage.Decode);
            
            var message = MessageHelper.Create("<test></test>");
            message.Context.Promote("Source","http://tempuri.org","Test");

            Assert.IsNull(message.Context.Read("Destination", "http://tempuri.org"));

            var output = pipeline.Execute(message);

            Assert.AreEqual(1,output.Count);

            Assert.IsTrue(output[0].Context.IsPromoted("Destination", "http://tempuri.org"));
        }
    }
}
