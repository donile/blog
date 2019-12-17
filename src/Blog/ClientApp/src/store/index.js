import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        authenticated: false,
    },
    mutations: {
        signIn(currentState){
            currentState.authenticated = true;
        },
        signOut(currentState){
            currentState.authenticated = false;
        }
    }
})