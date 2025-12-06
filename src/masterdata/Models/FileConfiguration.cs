using System.Collections.Generic;


namespace masterdata.Models
{
    public class FileConfiguration
    {
        public string FileNameExcel { get; set; } //= "ou_cleaning_database.xlsx";
        public string FileNameTxT { get; set; }//= "ou_cleaning_input.txt";
        public List<string> ExcelHeader { get; set; }// = new List<string>() { "u_org", "company", "company_desc", "pred_attr", "attr_prev_cod", "attr_prev_desc", "perimeter", "perimeter_desc", "bus_line_cod_1", "bus_line_desc_1", "bus_line_cod_2", "bus_line_desc_2", "bus_line_cod_3", "bus_line_desc_3", "bus_line_cod_4", "bus_line_desc_4", "bus_line_cod_5", "bus_line_desc_5", "serv_cod_1", "serv_desc_1", "serv_cod_2", "serv_desc_2", "serv_cod_3", "serv_desc_3", "serv_cod_4", "serv_desc_4", "serv_cod_5", "serv_desc_5", "staff_cod_1", "staff_desc_1", "staff_cod_2", "staff_desc_2", "staff_cod_3", "staff_desc_3", "staff_cod_4", "staff_desc_4", "staff_cod_5", "staff_desc_5" };

        public FileConfiguration() { }
    }
}