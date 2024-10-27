public class Insuree
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
   
}

public class InsureeController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // Create action for adding an insuree
    [HttpPost]
    public ActionResult Create(Insuree insuree)
    {
        if (ModelState.IsValid)
        {
            insuree.Quote = CalculateQuote(insuree);
            db.Insurees.Add(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(insuree);
    }

    private decimal CalculateQuote(Insuree insuree)
    {
        decimal quote = 50m;

        
        if (insuree.Age <= 18) quote += 100m;
        else if (insuree.Age >= 19 && insuree.Age <= 25) quote += 50m;
        else if (insuree.Age >= 26) quote += 25m;

       
        if (insuree.CarYear < 2000) quote += 25m;
        if (insuree.CarYear > 2015) quote += 25m;

       
        if (insuree.CarMake.ToLower() == "porsche")
        {
            quote += 25m;
            if (insuree.CarModel.ToLower() == "911 carrera") quote += 25m;
        }

        quote += insuree.SpeedingTickets * 10m;

        if (insuree.HasDUI) quote *= 1.

        if (insuree.IsFullCoverage) quote *= 1.5m;

        return quote;
    }

    public ActionResult Admin()
    {
        var insurees = db.Insurees.ToList();
        return View(insurees);
    }
}



<h2>Create Insuree</h2>

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    < div class= "form-horizontal" >
         @Html.ValidationSummary(true, "", new { @class = "text-danger" })
 

         < div class= "form-group" >
  
              < div class= "col-md-offset-2 col-md-10" >
   
                   < input type = "submit" value = "Create" class= "btn btn-primary" />
        
                    </ div >
        
                </ div >
        
            </ div >
}

@model IEnumerable<YourNamespace.Models.Insuree>

<h2>Admin View - All Quotes</h2>

<table class= "table" >
    < thead >
        < tr >
            < th > First Name </ th >
   
               < th > Last Name </ th >
      
                  < th > Email </ th >
      
                  < th > Quote </ th >
      
              </ tr >
      
          </ thead >
      
          < tbody >
              @foreach(var insuree in Model)
        {
            < tr >
                < td > @insuree.FirstName </ td >
                < td > @insuree.LastName </ td >
                < td > @insuree.Email </ td >
                < td > @insuree.Quote.ToString("C") </ td >
            </ tr >
        }
    </ tbody >
</ table >

