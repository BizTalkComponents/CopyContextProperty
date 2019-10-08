using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;

namespace BizTalkComponents.CopyContextProperty.Tests.UnitTests
{
    [TestClass]
    public class CopyContextPropertyWithNullCoalescingTests
    {
        [TestMethod]
        public void TestCopyPropertyWithNullCoalescing()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            var component = new PipelineComponents.CopyContextProperty.CopyContextPropertyWithNullCoalescing
            {
                FirstProperty= "http://tempuri.org#First",
                SecondProperty = "http://tempuri.org#Second",
                DestinationProperty = "http://tempuri.org#Destination"
            };
            pipeline.AddComponent(component, PipelineStage.Decode);
            var message = MessageHelper.Create("<test></test>");
            message.Context.Promote("Second", "http://tempuri.org", "Second");

            Assert.IsNull(message.Context.Read("First", "http://tempuri.org"));

            var output = pipeline.Execute(message);

            Assert.AreEqual(1, output.Count);

            Assert.IsTrue(output[0].Context.IsPromoted("Destination", "http://tempuri.org"));

        }
        [TestMethod]
        public void TestCopyPropertyWithNullCoalescingWhenBothAreNull()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            var component = new PipelineComponents.CopyContextProperty.CopyContextPropertyWithNullCoalescing
            {
                FirstProperty = "http://tempuri.org#First",
                SecondProperty = "http://tempuri.org#Second",
                DestinationProperty = "http://tempuri.org#Destination"
            };
            pipeline.AddComponent(component, PipelineStage.Decode);
            var message = MessageHelper.Create("<test></test>");

            Assert.IsNull(message.Context.Read("First", "http://tempuri.org"));
            Assert.IsNull(message.Context.Read("Second", "http://tempuri.org"));
            MessageCollection output=null;
            try
            {
                output = pipeline.Execute(message);
            }
            catch { }
            Assert.IsNull(output);

        }
    }
}
