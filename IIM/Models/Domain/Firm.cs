using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace IIM.Models.Domain
{
    public class Firm
    {
        #region Attributes

        #endregion

        #region Properties
        public int Id { get; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        #endregion

        #region Constructors
        public Firm()
        {
        }
        #endregion

        #region Actions

        #endregion
    }
}