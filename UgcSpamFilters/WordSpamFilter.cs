using System;
using System.Collections.Generic;
using System.Configuration;
using Tridion.ContentDelivery.AmbientData;
using Tridion.ContentDelivery.UGC.Web.Model;
using Tridion.ContentDelivery.UGC.Web.Utilities;
using Tridion.ContentDelivery.UGC.WebService;

namespace UGCSample
{
  public class WordsSpamFilter : SpamFilter
  {
    // Collect a list of disallowed words

    IList<string> spamWords = new List<string>();
    
    public WordsSpamFilter()
    {
      bool hasExcludes = true;
      int i = 1;
      while (hasExcludes)
      {
        string spamFilterWord = ConfigurationManager.AppSettings["Comment.SpamFilter.Exclude" + i];
        if (!string.IsNullOrEmpty(spamFilterWord))
        {
          spamWords.Add(spamFilterWord);
        }
        else
        {
          hasExcludes = false;
        }
        i++;
      }
    }

    /// <summary>
    /// Validate a comment.
    /// </summary>
    /// <param name="claimStore">The Claim Store from the current request.</param>
    /// <param name="comment">The comment to validate.</param>
    /// <returns>A valid comment.</returns>
    /// <exception cref="SpamFilterException">If comment is regarded as spam.</exception>
    public Comment ValidateComment(ClaimStore claimStore, Comment comment)
    {
      string commentContent = comment.Content;

      if (String.IsNullOrEmpty(commentContent))
      {
        throw new SpamFilterException("Comment cannot be empty.");
      }
      else
      {
        foreach (string spamWord in spamWords) 
        {
          if (commentContent.Contains(spamWord))
          {
            throw new SpamFilterException("Comment rejected: contains the word '" + spamWord + "'");
          }
        }
        // The comment contains none of the configured words, so it is acceptable.
        return comment;
      }
    }
  }
}