export default class HTTPRequestManager {
    constructor() {
        this.xhr = new XMLHttpRequest();
        this.xhr.timeout = 15000; // 15 seconds
    }

    GET(url, onLoad, onError) {
        this.xhr.open("GET", new URL(url));
        
        this.xhr.onload = () => onLoad(this.xhr.response, this.xhr.status);
        this.xhr.onerror = onError;

        this.xhr.send();
    }

    POST(url, onLoad, onError, isJson, body) {
        this.xhr.open("POST", new URL(url));
        
        this.xhr.onload = () => onLoad(this.xhr.response, this.xhr.status);
        this.xhr.onerror = onError;

        if (isJson) {
            this.xhr.setRequestHeader('Content-Type', 'application/json');
        }

        this.xhr.send(body);
    }

    PUT(url, onLoad, onError, isJson, body) {
        this.xhr.open("PUT", new URL(url));
        
        this.xhr.onload = () => onLoad(this.xhr.response, this.xhr.status);
        this.xhr.onerror = onError;

        if (isJson) {
            this.xhr.setRequestHeader('Content-Type', 'application/json');
        }

        this.xhr.send(body);
    }

    PATCH(url, onLoad, onError, isJson, body) {
        this.xhr.open("PATCH", new URL(url));
        
        this.xhr.onload = () => onLoad(this.xhr.response, this.xhr.status);
        this.xhr.onerror = onError;

        if (isJson) {
            this.xhr.setRequestHeader('Content-Type', 'application/json');
        }

        this.xhr.send(body);
    }

    DELETE(url, onLoad, onError, isJson, body) {
        this.xhr.open("DELETE", new URL(url));
        
        this.xhr.onload = () => onLoad(this.xhr.response, this.xhr.status);
        this.xhr.onerror = onError;

        if (isJson) {
            this.xhr.setRequestHeader('Content-Type', 'application/json');
        }

        this.xhr.send(body);
    }
}