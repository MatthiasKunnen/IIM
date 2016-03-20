using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IIM.Models.Domain
{
    public class TargetGroup
    {
        #region Attributes

        #endregion

        #region Properties
        [Key]
        public int Id { get; }

        public string Name { get; set; }

        #endregion

        #region Constructors
        public TargetGroup()
        {
        }
        #endregion

        #region Actions
#endregion
    }
}