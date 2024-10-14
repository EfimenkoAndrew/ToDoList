using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Common;


public record PageResponse<T>([property: Required] int Total, [property: Required] T Results) where T : class;