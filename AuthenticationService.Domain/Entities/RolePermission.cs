public class RolePermission
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public DateTime AssignedDate { get; set; }

    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}