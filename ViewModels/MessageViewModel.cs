using System.ComponentModel.DataAnnotations;

namespace SignalRDev.ViewModels
{
    public class MessageViewModel
    {
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mesaj içeriği zorunludur.")]
        [Display(Name = "Mesaj")]
        public string Body { get; set; }

        public string SenderId { get; set; }
         
        [Display(Name = "Alıcı")]
        public string ReceiverId { get; set; }
    }
} 