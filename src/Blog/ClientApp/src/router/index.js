import Vue from 'vue';
import VueRouter from 'vue-router';

import HomePage from '../components/HomePage';
import AdminHome from '../components/AdminHome';
import UserSignIn from '../components/UserSignIn';

Vue.use(VueRouter);

export default new VueRouter({
    mode: "history",
    routes: [
        { path: "/", component: HomePage },
        { path: "/admin", component: AdminHome },
        { name: "UserSignIn", path: "/admin/signin", component: UserSignIn },
        { path: "*", redirect: "/" }
    ]
})