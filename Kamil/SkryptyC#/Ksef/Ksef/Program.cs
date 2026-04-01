using Ksef;
using Ksef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;

using var db = new AppDbContext();


//modelBuilder.Entity<>().Ignore(p => p.AddressDetails.Country);

// Add data
//przykladowe dane faktury
foreach (var f in SampleData.GetFaktury())
    db.Faktura.Add(f);

db.SaveChanges();

// Read data

//foreach (var user in users)
//{
//    Console.WriteLine(user.Name);
//}
