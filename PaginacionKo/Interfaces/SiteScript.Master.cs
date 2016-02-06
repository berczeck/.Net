using System;
using System.Web.UI;

namespace WebApplicationIntegrada.Interfaces
{
    public partial class SiteScript : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {
        }

        protected void btnLeerData_Click(object sender, EventArgs e)
        {
            lblValor.Text = hfIdentificadores.Value;
        }
    }
}