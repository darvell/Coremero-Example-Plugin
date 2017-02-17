using System;
using System.Threading.Tasks;
using Coremero;
using Coremero.Commands;
using Coremero.Messages;

namespace Coremero.Plugin.Example
{
    public class ExamplePlugin : IPlugin
    {
        // First, a simple string return.
        [Command("hello")]
        public string SayHello()
        {
            return "Hello!";
        }

        // Now let's add the IInovcationContext to our method as well as a string.
        // The IInvocationContext holds who/what raised the message and what client it came from.
        // The string parameter is what text was provided after the command.
        [Command("greet", Arguments = "User")]
        public string GreetUser(IInvocationContext context, string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                // Get the user from the invocation context if a user wasn't provided as an argument.
                user = context?.User.Name;
            }
            return $"Hello {user}!";
        }

        // Now let's return an image. We have to now return an IMessage object. There is a standard message object to help create these.
        [Command("image")]
        public IMessage FakeImage()
        {
            return Message.Create("This is a fake image.", new FileAttachment("PATH_TO_IMAGE_HERE.jpg"));
        }

        // Now let's inspect an incoming full message object instead of just getting a stripped version.
        // We can inspect the attachment list to see if there were any attachments and inform the user how many there were.
        [Command("attachmentcheck")]
        public string AttachmentChecker(IMessage message)
        {
            if (message.Attachments?.Count > 0)
            {
                return $"{message.Attachments.Count} attachments came with your message.";
            }
            return "No attachments came with your message.";
        }

        // You can also use async methods if you wish just fine.
        [Command("hellodelay")]
        public async Task<string> SayHelloWithDelay()
        {
            await Task.Delay(5000);
            return "Hello!";
        }

    }
}
