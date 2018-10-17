export class ExerciseScoreModel
{
    public exerciseId: number = 0;
    public leftNumber:number = 0;
    public rightNumber:number = 0;
    public mathOperator: number = 0;
    public answer: string = '';
    public correctAnswerGiven: boolean = false;
    public userId: string = '';
    public level: number = 1;
    public highScore: number = 0;
}