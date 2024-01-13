using Synergy.Catalogue;
using Synergy.Documentation.Annotations;

namespace Synergy.Documentation.Tests.Comments;

[CodeFilePath]
public class NoteTests
{
    [Fact]
    public void ShowOff()
    {
        this.Comment("Here you have full sample of comments as code")
            .Because("I want to show you how to use them")
            .DoNothing("because this is just a comment")
            .Because("I want to show you how to use them")
            .DoNotThrowException("because this is just a comment")
            .Because("I want to show you how to use them")
            .Then("I want to show you how to use them")
            .But("I want to show you how to use them")
            .Therefore("I want to show you how to use them")
            .Otherwise("I want to show you how to use them")
            .Moreover("I want to show you how to use them")
            .Reference("https://stackoverflow.blog/2021/12/23/best-practices-for-writing-code-comments/");
    }
    
    [Fact]
    public void CommentsTests()
    {
        try
        {
            var list = new List<string>(10.Because("we do not waste memory when we know exact size of the list"));

            if (list.IsEmpty())
                this.DoNothing("because we do not have any items in the list");
        }
        catch
        {
            this.DoNotThrowException("when something bad happens here")
                .Because("this code should always work")
                .Therefore("we should log the exception at least");
        }
    }
}