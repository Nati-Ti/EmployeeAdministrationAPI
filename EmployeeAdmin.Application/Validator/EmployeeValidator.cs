//using FluentValidation;
//using EmployeeAdmin.Application.Command;

//namespace ToDo.Application.Validators
//{
//    public class CreateToDoItemCommandValidator : AbstractValidator<CreatePositionCommand>
//    {
//        public CreatePositionCommandValidator()
//        {
//            RuleFor(x => x.InputDto.Name)
//                .NotEmpty().WithMessage("Name is required.")
//                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

//            RuleFor(x => x.InputDto.ParentId)
//                .NotEmpty().WithMessage("ParentId is required.");

//            RuleFor(x => x.InputDto.Description)
//                .NotEmpty().WithMessage("Description is required.")
//                .MaximumLength(100).WithMessage("Description cannot exceed 100 characters.");
//        }
//    }

//    public class UpdateToDoItemProgressCommandValidator : AbstractValidator<UpdatePositionCommand>
//    {
//        public UpdatePositionCommandValidator()
//        {
//            RuleFor(x => x.id)
//                .NotEmpty().WithMessage("Id is required.");

//            RuleFor(x => x.inputDto.Name)
//                .NotEmpty().WithMessage("Name is required.")
//                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

//            RuleFor(x => x.inputDto.ParentId)
//                .NotEmpty().WithMessage("ParentId is required.");

//            RuleFor(x => x.inputDto.Description)
//                .NotEmpty().WithMessage("Description is required.")
//                .MaximumLength(100).WithMessage("Description cannot exceed 100 characters.");
//        }
//    }

//}