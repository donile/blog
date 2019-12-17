import store from '../store';

export default class AuthenticationService {
    
    constructor(){
        this.store = store;
    }

    isAuthenticated(){
        return store.state.authenticated;
    }
}

