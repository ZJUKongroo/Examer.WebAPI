namespace Examer.Models;

public class ExamInheritance : ModelBase
{
    public Guid InheritedExamId { get; set; } // Parent Exam
    public Guid InheritingExamId { get; set; } // Children Exam
    public Exam InheritedExam { get; set; } = null!;
    public Exam InheritingExam { get; set; } = null!;
}
