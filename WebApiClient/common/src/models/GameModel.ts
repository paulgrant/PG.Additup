export default interface ExerciseScoreModel
{
    exerciseId: number,
    userId: string,
    time: number,
    correctExercises: number,
    skillLevel: number,
    timerOn: boolean,
    resetExercise: boolean,
    startButtonVisible:boolean,
    showExercises: boolean
}