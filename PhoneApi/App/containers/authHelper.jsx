export default {
    saveAuth: (login, token) => {
        sessionStorage.setItem(constants.tokenKey, JSON.stringify({ login: login, access_token: token }));
    },

    clearAuth: () => {
        sessionStorage.removeItem(constants.tokenKey);
    },

    getLogin: () => {
        let item = sessionStorage.getItem(constants.tokenKey);
        let login = '';
        if (item) {
            login = JSON.parse(item).login;
        }
        return login;
    },

    isLogged: () => {
        let item = sessionStorage.getItem(constants.tokenKey);
        if (item) {
            return true;
        } else {
            return false;
        }
    },

    getToken: () => {
        let item = sessionStorage.getItem(constants.tokenKey);
        let token = null;
        if (item) {
            token = JSON.parse(item).access_token;
        }
        return token;
    }
}