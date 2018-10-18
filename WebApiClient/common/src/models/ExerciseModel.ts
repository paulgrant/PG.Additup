export default interface ExerciseModel
{
    exerciseId: number,
    leftNumber:number,
    rightNumber:number,
    mathOperator: number,
    answer: string,
    correctAnswerGiven: boolean,
    userId: string,
}