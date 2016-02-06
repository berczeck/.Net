using System;
using System.Web.UI;

namespace WebApplicationIntegrada.Interfaces
{
    public partial class BandejaProductos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHijo.Items.Clear();
            for (int i = 0; i < 3; i++)
            {
                ddlHijo.Items.Add(ddlPrincipal.SelectedItem.Value);
            }
        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {
        }
    }
}