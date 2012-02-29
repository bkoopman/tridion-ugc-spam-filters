using System.Configuration;
using JelleDruyts.Mollom.Client;
using Tridion.ContentDelivery.AmbientData;
using Tridion.ContentDelivery.UGC.Web.Model;
using Tridion.ContentDelivery.UGC.Web.Utilities;
using Tridion.ContentDelivery.UGC.WebService;

namespace UGCSample
{
  public class MollomSpamFilter : SpamFilter
  {
    MollomClient client;
    
    public MollomSpamFilter()
    {
      this.client = new MollomClient(
        ConfigurationManager.AppSettings["Mollom.privateKey"],
        ConfigurationManager.AppSettings["Mollom.publicKey"]
      );
    }

    public Comment ValidateComment(ClaimStore claimStore, Comment comment)
    {
      ContentCheck result = client.CheckContent(string.Empty, comment.Content);
      
      if (result.Classification == ContentClassification.Spam)
      {
        throw new SpamFilterException("Comment rejected by Mollom, Quality: " + result.Quality);
      }
      
      return comment;
    }
  }
}