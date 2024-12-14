using System.ComponentModel.DataAnnotations;

using AccountWebAPI.Database.Models;

namespace AccountWebAPI.Dtos;

public record PatchAccountDto(
    [param: StringLength(12)]
    string? AccountNumber,

    DateTime? StartDate,

    DateTime? EndDate,

    string? Address,

    [param: Range(1, 1000)]
    double? Area
) : BasePatchDto<Account>;

public record CreateAccountDto(
    [param: Required]
    [param: Length(12, 12)]
    string AccountNumber,

    [param: Required]
    DateTime StartDate,

    [param: Required]
    DateTime EndDate,

    [param: Required]
    [param: MinLength(10)]
    string Address,

    [param: Required]
    [param: Range(1, 1000)]
    double Area
) : BaseCreateDto<Account>;