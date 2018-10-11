import React from 'react';
import axios from 'axios';
import MathOperator from '../game/mathOperator';
import * as Constants from '../../utils/constants';

class Game extends React.Component {
    state = {
        games: [],
        currentGame: {
            leftNumber:'',
            rightNumber:'',
            mathOperator: '',
        },
        answer: '',
        isLoading: false,
        validationError: false,
        formSubmittedError: false,
        showForm: true,
        formSubmittedSuccess: false,
        errorText: ''
    }

    componentDidMount() {
        var self = this;
        axios.get(Constants.BASE_URL + 'api/exercise')
        .then(res => {
            self.setState({ currentGame: res.data });
            var _stategames = this.state.games.concat([res.data]);
            self.setState({ games: _stategames });
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        this.setState({ isLoading: true });
        this.setState({ formSubmittedError: false });
        this.validateFields();

        if (this.state.validationError === false) {
            this.setState({ isLoading: true });
            var bodyFormData = new FormData();
            bodyFormData.set('value', this.state.answer);
            let data = {
                value:this.state.answer,
              };
            var self = this;
            axios({
                method: 'post',
                url: Constants.BASE_URL + 'api/exercise',
                data: data,
                config: { headers: { 'Content-Type': 'application/json' } }
            })
            .then(function (response) {
                //handle success
                console.log(response);
                self.setState({ isLoading: false });
            })
            .catch(function (response) {
                //handle error
                console.log(response);
                self.setState({ isLoading: false });
            });
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
                <h1>Add it up!</h1>
                <form className="form-inline" onSubmit={this.handleSubmit} id="gameForm">
                    {this.state.validationError === true ? (
                        <div className="alert alert-danger">
                            Invalid entry!
            </div>
                    ) : null}
                    {this.state.formSubmittedSuccess === true ? (
                        <div className="alert alert-success">
                            Successfully answered
            </div>
                    ) : null}
                    {this.state.formSubmittedError === true ? (
                        <div className="alert alert-danger">
                            Incorrect!
            </div>
                    ) : null}
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
                        <button id="my-account-save-button" className={"col-xs-8 colour8 bgcolour2 " + (this.props.hideSubmitButton === true ? 'hidden' : '')} type="submit" disabled={this.state.isLoading}>
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
            </div>
        );
    }
}

export default Game;