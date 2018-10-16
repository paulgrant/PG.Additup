import React from 'react';
import axios from 'axios';
import MathOperator from '../game/mathOperator';
import * as Constants from '../../utils/constants';
import PropTypes from 'prop-types';

class Exercise extends React.Component {
    state = {
        games: [],
        currentGame: {
            leftNumber:'',
            rightNumber:'',
            mathOperator: '',
            userId: '',
            exerciseId: 0
        },
        answer: '',
        isLoading: false,
        validationError: false,
        formSubmittedError: false,
        showForm: true,
        formSubmittedSuccess: false,
        errorText: '',
        isCorrectAnswer: false
    }

    componentWillReceiveProps(nextProps) {
        if(nextProps.reset === true){
            //this.setState({time: nextProps.resetExercise});
            //this.getQuestion();
        }
        
      }

    componentDidMount() {
        this.getQuestion();
    }

    getQuestion = () => {
        var self = this;
        return axios.get(Constants.BASE_URL + 'api/exercise')
        .then(res => {
            if (typeof self.props.setUserId === 'function') {
                self.props.setUserId(res.data.userId);
            }
            self.setState({ currentGame: res.data });
            var _stategames = self.state.games.concat([res.data]);
            self.setState({ games: _stategames });
        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })
        .then(function () {
            // always executed
            if (self.state.currentGame === null || self.state.currentGame === "") {
                self.props.onSuccess(-1);
            }
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.setState({ isLoading: true });
        this.setState({ formSubmittedError: false });
        this.validateFields();

        if (this.state.validationError === false) {
            var self = this;
            return axios({
                method: 'post',
                url: Constants.BASE_URL + 'api/exercise',
                data: {
                    userId: this.state.currentGame.userId,
                    exerciseId: this.state.currentGame.exerciseId,
                    leftNumber:this.state.currentGame.leftNumber,
                    rightNumber:this.state.currentGame.rightNumber,
                    mathOperator:this.state.currentGame.mathOperator,
                    answer:this.state.answer,
                  },
                config: { 
                    headers: { 'Content-Type': 'application/json', 'Accept': 'application/json' } 
                }
            })
            .then(function (response) {
                //handle success
                console.log(response);
                self.answeredCorrectly(response.data.correctAnswerGiven);
            })
            .catch(function (response) {
                //handle error
                console.log(response);
                self.setState({formSubmittedSuccess: false});
                if (typeof self.props.onSuccess === 'function') {
                    self.props.onSuccess(-1);
                }
            });
        };
    }

    answeredCorrectly = (isCorrect) => {
        this.setState({ formSubmittedSuccess: true});
        this.setState({ isCorrectAnswer: isCorrect});
        if(this.state.isCorrectAnswer===true){
            if (typeof this.props.onSuccess === 'function') {
                this.props.onSuccess(1);
            }
            this.setState({ isLoading: false });
            this.setState({ answer: '' });
            this.getQuestion();
        }
    }

    validateFields = () => {
        if ((this.state.name === null || this.state.name === "")
            || (this.state.endDate === null || this.state.endDate === "")
            || (this.state.startDate === null || this.state.startDate === "")) {
            this.setState({ validationError: true });
        }
        else {
            this.setState({ validationError: false });
        }
    }

    handleChange = (e) => {
        var change = {};
        change[e.target.name] = e.target.value;
        this.setState(change);
    }

    render() {
        return (
            <div>
                <form className="form" onSubmit={this.handleSubmit} id="gameForm">
                    { (this.state.currentGame) ? (
                    <div className="form-group">
                        <label className="mr-sm-2">{this.state.currentGame.leftNumber}</label>
                        <label className="mr-sm-2"><MathOperator operator={this.state.currentGame.mathOperator}></MathOperator></label>
                        <label className="mr-sm-2">{this.state.currentGame.rightNumber}</label>
                        <div className="col-sm-6 col-xs-12">
                            <input id="game-form-answer-input" type="number" name="answer" required
                                placeholder="Min:-9999, Max:9999" min="-9999" max="9999"
                                value={this.state.answer} onChange={this.handleChange} />
                        </div>
                        <button id="my-account-save-button" className={"col-xs-8 colour8 bgcolour2 " } type="submit" disabled={this.state.isLoading}>
                            {this.state.isLoading === false &&
                                <div>
                                    Submit Answer <i className="fa fa-caret-right" aria-hidden="true"></i>
                                </div>
                            }
                            {this.state.isLoading === true &&
                                <div className="spinner">
                                    <div className="bounce1"></div>
                                    <div className="bounce2"></div>
                                    <div className="bounce3"></div>
                                </div>
                            }
                        </button>
                    </div>
                    ) : (
                        <div>No exercises available</div>
                    ) }
                </form>
                {this.state.validationError === true ? (
                <div className="alert alert-danger">Invalid entry!</div>
                ) : null}
                {this.state.formSubmittedSuccess === true ? (
                    <div className="alert alert-success">Successfully answered</div>
                ) : null}
                {this.state.formSubmittedError === true ? (
                <div className="alert alert-danger">Incorrect!</div>
                ) : null}
            </div>
        );
    }
}

Exercise.propTypes = {
    reset: PropTypes.bool,
    onSuccess: PropTypes.func,
    setUserId: PropTypes.func
}

export default Exercise;