using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GioHang : System.Web.UI.Page
{
    static DataTable tbGioHang = new DataTable();

    private void load_data()
    {
        tbGioHang = (DataTable)Session["GioHang"];
        GridView1.DataSource = tbGioHang;
        GridView1.DataBind();

        string strnumber = tbGioHang.Compute("Sum(ThanhTien)", "").ToString();
        lblTongTien.Text = "Tổng tiền:   " + strnumber;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["TenTaiKhoan"]) == "")
        {
            Response.Redirect("DangNhap.aspx");
        }
        else
        {
            if (IsPostBack == false)
                load_data();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        load_data();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int SL = int.Parse((GridView1.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text);
        tbGioHang.Rows[e.RowIndex]["SoLuong"] = SL;
        GridView1.EditIndex = -1;
        load_data();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        tbGioHang.Rows.RemoveAt(e.RowIndex);
        GridView1.EditIndex = -1;
        load_data();
        lblTongTien.Text = "Không có sản phẩm nào";
        
    }
   
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        load_data();
    }
}
    