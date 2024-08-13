using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace OnlineVotingS.API.Models;
public class CompleteElectionViewModel
{
    public int SelectedElectionID { get; set; } // The ID of the election to complete
    public List<SelectListItem> OngoingElections { get; set; } = new List<SelectListItem>(); // Dropdown of ongoing elections
}
