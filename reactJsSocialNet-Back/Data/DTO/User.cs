namespace reactJsSocialNet_Back.Data.DTO
{
    public record UserDto(
        int Id,
        string Username,
        UserProfileDto Profile
    );

    public record UserProfileDto(
        string FirstName,
        string LastName,
        DateTime BirthDate,
        string City,
        string AvatarUrl
    );
}
