using System.ComponentModel.DataAnnotations;

public class TipRecord
{
    public int TableId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Účet musí být větší než 0.")]
    public decimal BillAmount { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Spropitné musí být větší než 0.")]
    public decimal TipAmount { get; set; }

    public int TipPercent { get; set; }

    [Required(ErrorMessage = "Email je povinný.")]
    [EmailAddress(ErrorMessage = "Neplatný formát emailu.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Hodnocení je povinné.")]
    [Range(1, 5, ErrorMessage = "Hodnocení musí být 1 až 5.")]
    public int Rating { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime Updated { get; set; }
    public bool IsActive { get; set; }
}
