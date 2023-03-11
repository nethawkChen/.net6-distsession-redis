using System.ComponentModel.DataAnnotations;

namespace DistSession.ViewModel {
    public class UserData {
        [Display(Name = "員工編號")]
        public string? AgentId { get; set; }

        [Display(Name = "員工姓名")]
        public string? AgentName { get; set; }

        [Display(Name = "部門編號")]
        public string? DeptID { get; set; }

        [Display(Name = "部門名稱")]
        public string? DeptName { get; set; }

        [Display(Name = "角色代碼")]
        public string? RoleID { get; set; }

        [Display(Name = "角色")]
        public string? RolenName { get; set; }

        [Display(Name = "狀態代碼")]
        public string? StatusID { get; set; }

        [Display(Name = "登入時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime LoginTime { get; set; }
    }
}
