namespace HajurKoCarRental.Shared.Routing;

public class ApiRoutes
{
    /// <summary>
    ///     `POST /api/auth/register` - Register a new customer account.
    /// </summary>
    public const string RegisterRoute = "/api/auth/register/";

    /// <summary>
    ///     `POST /api/auth/login` - Log in to an existing customer account.
    /// </summary>
    public const string LoginRoute = "/api/auth/login/";

    /// <summary>
    ///     `POST /api/auth/forgot-password` - Send a password reset link to the customer's email address.
    /// </summary>
    public const string ForgotPasswordRoute = "/api/auth/reset-password";

    /// <summary>
    ///     `POST /api/auth/reset-password` - Reset the customer's password.
    /// </summary>
    public const string ResetPasswordRoute = "/api/auth/reset-password";
    
    /// <summary>
    ///     `POST /api/auth/logout` - Log out current user.
    /// </summary>
    // logout modifies db state, and as per REST guidelines, it cant be a get request
    public const string LogoutRoute = "/api/auth/logout";

    /// <summary>
    ///     `POST /api/documents/upload` - Upload a customer's driving license or citizenship paper.
    /// </summary>
    public const string DocumentsUploadRoute = "/api/documents/upload";

    /// <summary>
    ///     `GET /api/documents/download` - Download a customer's previously uploaded documents.
    /// </summary>
    public const string DocumentsDownloadRoute = "/api/documents/download";

    /// <summary>
    ///     `POST /api/cars` - Add a new car to the rental inventory.
    ///     `GET /api/cars` - Get a list of all available cars for rent.
    /// </summary>
    public const string CarsRoute = "/api/cars";

    /// <summary>
    ///     `PUT /api/cars/{id}` - Update an existing car's information.
    ///     `DELETE /api/cars/{id}` - Remove a car from the rental inventory.
    ///     `GET /api/cars/{id}` - Get details about a specific car.
    /// </summary>
    public const string CarRoute = "/api/cars/{id}";
    
    /// <summary>
    ///     `POST /api/users` - Add users.
    ///     `GET /api/users` - Get a list of all users.
    /// </summary>
    public const string UsersRoute = "/api/users";
    
    /// <summary>
    ///     `GET /api/users` - Get a list of all users that are staff.
    /// </summary>
    public const string StaffsRoute = "/api/staffs";

    /// <summary>
    ///     `GET /api/users/{id}` - Get details about a user.
    ///     `PUT /api/users/{id}` - Update the user.
    ///     `DELETE /api/users/{id}` - Delete user.
    /// </summary>
    public const string UserRoute = "/api/users/{id}";
    
    /// <summary>
    ///     `POST /api/rental-requests` - Create new rental requests.
    ///     `GET /api/rental-requests` - Get a list of all rental requests.
    /// </summary>
    public const string RentalRequestsRoute = "/api/rentalrequests";
    
    /// <summary>
    ///     `PUT /api/rental-request/{id}` - Update rental request.
    ///     `DELETE /api/rental-request/{id}` - Delete rental request.
    /// </summary>
    public const string RentalRequestRoute = "/api/rentalrequests/{id}";

    /// <summary>
    ///     `POST /api/rentals` - Create a rental.
    ///     `GET /api/rentals` - Get a list of all rentals.
    /// </summary>
    public const string RentalsRoute = "/api/rentals";

    /// <summary>
    ///     `GET /api/rentals/{id}` - Get details about a specific rental.
    ///     `PUT /api/rentals/{id}` - Update the status of a rental.
    ///     `DELETE /api/rentals/{id}` - Cancel a rental request.
    /// </summary>
    public const string RentalRoute = "/api/rentals/{id}";

    /// <summary>
    ///     `POST /api/damages` - Submit a damage request for a rented car.
    ///     `GET /api/damages` - Get a list of all damage requests.
    /// </summary>
    public const string DamagesRoute = "/api/damages";

    /// <summary>
    ///     `GET /api/damages/{id}` - Get details about a specific damage request.
    ///     `PUT /api/damages/{id}` - Update the status of a damage request.
    /// </summary>
    public const string DamageRoute = "/api/damages/{id}";

    /// <summary>
    ///     `POST /api/payments` - Submit a payment for a rental or repair bill.
    ///     `GET /api/payments` - Get a list of all payments.
    /// </summary>
    public const string PaymentsRoute = "/api/payments";

    /// <summary>
    ///     `GET /api/payments/{id}` - Get details about a specific payment.
    /// </summary>
    public const string PaymentRoute = "/api/payments/{id}";

    /// <summary>
    ///     `POST /api/offers` - Create a new special offer for customers.
    ///     `GET /api/offers` - Get a list of all current special offers.
    /// </summary>
    public const string OffersRoute = "/api/offers";

    /// <summary>
    ///     `GET /api/offers/{id}` - Get details about a specific special offer.
    /// </summary>
    public const string OfferRoute = "/api/offers/{id}";

    /// <summary>
    ///     `GET /api/customers` - Get a list of all registered customers.
    /// </summary>
    public const string CustomersRoute = "/api/customers";

    /// <summary>
    ///     `GET /api/customers/{id}` - Get details about a specific customer.
    ///     `PUT /api/customers/{id}` - Update a customer's information.
    ///     `DELETE /api/customers/{id}` - Remove a customer account.
    /// </summary>
    public const string CustomerRoute = "/api/customers/{id}";
}