using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentDelivery.UGC.WebService;
using Tridion.ContentDelivery.UGC.Web.Model;
using Tridion.ContentDelivery.UGC.Web.Utilities;
using Tridion.ContentDelivery.AmbientData;
using System.Configuration;

namespace UGCSample
{
  public class MollomSpamFilter : SpamFilter
  {
    public MollomSpamFilter()
    {
    }

    public Comment ValidateComment(ClaimStore claimStore, Comment comment)
    {
      return default(Comment);
    }
  }
}