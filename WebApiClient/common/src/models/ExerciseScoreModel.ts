export default interface ExerciseScoreModel
{
    exerciseId: number,
    leftNumber:number,
    rightNumber:number,
    mathOperator: number,
    answer: string,
    correctAnswerGiven: boolean,
    userId: string,
    level: number,
    highScore: number
}