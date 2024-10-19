using BehzadDara.BaseResult;

public class Program
{
    public static void Main()
    {
        // Sample fruit list
        var data = new List<string>
        {
            "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape",
            "Honeydew", "Kiwi", "Lemon", "Mango", "Nectarine", "Orange", "Papaya"
        };

        // Function to get paged data based on page size and page number
        PagedResult<string> GetPagedData(int pageSize, int pageNumber, List<string> allData)
        {
            // Total record count
            int totalCount = allData.Count;

            // Page number should start from 1; if negative or 0 is entered, set to 1
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            // Skip previous pages' data and take data according to page size
            var pagedData = allData
                .Skip((pageNumber - 1) * pageSize)  // Skip the data of previous pages
                .Take(pageSize)                    // Take data according to page size
                .ToList();

            // Return the result
            return PagedResult<string>.Create(pageSize, pageNumber, totalCount, pagedData);
        }

        // 1st page (First 5 records)
        var page1 = GetPagedData(5, 1, data);
        Console.WriteLine("1st Page (First 5 records):");
        PrintPagedResult(page1);

        // 2nd page (Next 5 records)
        var page2 = GetPagedData(5, 2, data);
        Console.WriteLine("\n2nd Page (Next 5 records):");
        PrintPagedResult(page2);

        // 3rd page (Remaining records)
        var page3 = GetPagedData(5, 3, data);
        Console.WriteLine("\n3rd Page (Remaining records):");
        PrintPagedResult(page3);
    }

    //var successResult = Result.Success("Operation completed successfully.");

    /*
    // Result<T> için örnek başarı sonucu
    var valueResult = Result.Success<string>("Returned Value", "Operation succeeded with value.");
    Console.WriteLine("\nResult<T> Success Example:");
        Console.WriteLine($"IsSuccess: {valueResult.IsSuccess}");
        Console.WriteLine($"StatusCode: {valueResult.StatusCode}");
        Console.WriteLine($"Value: {valueResult.Value}");
        Console.WriteLine("Messages:");
        foreach (var message in valueResult.SuccessMessages)
        {
            Console.WriteLine($"- {message}");
        }

// Hata durumunu test etmek için örnek veri
var errorMessages = new Dictionary<string, string[]>
        {
            { "Field1", new[] { "Error message 1" } },
            { "Field2", new[] { "Error message 2" } }
        };
var errorResult = new Result();
errorResult.BadRequest(errorMessages, "There was an error with the request.");

Console.WriteLine("\nResult Error Example:");
Console.WriteLine($"IsSuccess: {errorResult.IsSuccess}");
Console.WriteLine($"StatusCode: {errorResult.StatusCode}");
Console.WriteLine("ErrorMessages:");
foreach (var message in errorResult.ErrorMessages)
{
    Console.WriteLine($"- {message}");
}

*/




// Helper function to print the PagedResult to the console
public static void PrintPagedResult(PagedResult<string> pagedResult)
    {
        Console.WriteLine($"PageNumber: {pagedResult.PageNumber}");
        Console.WriteLine($"PageSize: {pagedResult.PageSize}");
        Console.WriteLine($"TotalCount: {pagedResult.TotalCount}");
        Console.WriteLine("Data:");
        foreach (var item in pagedResult.Data)
        {
            Console.WriteLine($"- {item}");
        }

    }
}