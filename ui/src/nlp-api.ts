//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class NlpBaseClient {
    static URL_START = /^[^:]*:\/\/[^/]*\/?/.exec(location.href)![0];

  async transformOptions(options: RequestInit) {
        if (!options.credentials) options.credentials = 'same-origin';
        if (!options.headers) options.headers = {};
        if(Array.isArray(options.headers)) {
            if(!options.headers.some(r => r[0].localeCompare('Accept',void 0, {sensitivity: 'base'}) === 0))
                options.headers.push(['Accept','application/json']);
        }
        else if (typeof(options.headers.has) === 'function' && options.headers.has('Accept'))
            (<Headers>options.headers).set('Accept','application/json');
        else if (!(<Record<string, string>>options.headers).Accept)
            (<Record<string, string>>options.headers).Accept = 'application/json';
        return options;
  }

  async transformResult(url: string, response: Response, /** @type {(Response) => Promise} */processor: ((r: Response) => Promise<any>)) {
        if (response.status !== 401 || !response.url.startsWith(NlpBaseClient.URL_START) || response.headers.get('Content-Type') !== 'application/json')
            return await processor(response);

        location.href = (await response.json()).redirectTo;
        throw new Error('NLP server requested authentication.');
    }
}

export interface IAuthClient {

    whoAmI(includeClaims: boolean | undefined): Promise<WhoAmIResponse>;

    challenge(requestGoogleSelectAccount: boolean | undefined): Promise<WhoAmIResponse>;

    login(password: string | undefined): Promise<void>;

    logout(): Promise<void>;

    setNewPassword(request: SetNewPasswordRequest): Promise<string>;
}

export class AuthClient extends NlpBaseClient implements IAuthClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    whoAmI(includeClaims: boolean | undefined): Promise<WhoAmIResponse> {
        let url_ = this.baseUrl + "/api/Auth/whoami?";
        if (includeClaims === null)
            throw new Error("The parameter 'includeClaims' cannot be null.");
        else if (includeClaims !== undefined)
            url_ += "includeClaims=" + encodeURIComponent("" + includeClaims) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processWhoAmI(_response));
        });
    }

    protected processWhoAmI(response: Response): Promise<WhoAmIResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = WhoAmIResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<WhoAmIResponse>(null as any);
    }

    challenge(requestGoogleSelectAccount: boolean | undefined): Promise<WhoAmIResponse> {
        let url_ = this.baseUrl + "/api/Auth/authenticate?";
        if (requestGoogleSelectAccount === null)
            throw new Error("The parameter 'requestGoogleSelectAccount' cannot be null.");
        else if (requestGoogleSelectAccount !== undefined)
            url_ += "requestGoogleSelectAccount=" + encodeURIComponent("" + requestGoogleSelectAccount) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processChallenge(_response));
        });
    }

    protected processChallenge(response: Response): Promise<WhoAmIResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = WhoAmIResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<WhoAmIResponse>(null as any);
    }

    login(password: string | undefined): Promise<void> {
        let url_ = this.baseUrl + "/api/Auth/login?";
        if (password === null)
            throw new Error("The parameter 'password' cannot be null.");
        else if (password !== undefined)
            url_ += "password=" + encodeURIComponent("" + password) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processLogin(_response));
        });
    }

    protected processLogin(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }

    logout(): Promise<void> {
        let url_ = this.baseUrl + "/api/Auth/logout";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processLogout(_response));
        });
    }

    protected processLogout(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }

    setNewPassword(request: SetNewPasswordRequest): Promise<string> {
        let url_ = this.baseUrl + "/api/Auth/set-new-password";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processSetNewPassword(_response));
        });
    }

    protected processSetNewPassword(response: Response): Promise<string> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<string>(null as any);
    }
}

export interface IImageClient {

    getUserLinkImage(linkId: number): Promise<FileResponse | null>;
}

export class ImageClient extends NlpBaseClient implements IImageClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getUserLinkImage(linkId: number): Promise<FileResponse | null> {
        let url_ = this.baseUrl + "/api/Image/user-link/{linkId}";
        if (linkId === undefined || linkId === null)
            throw new Error("The parameter 'linkId' must be defined.");
        url_ = url_.replace("{linkId}", encodeURIComponent("" + linkId));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/octet-stream"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetUserLinkImage(_response));
        });
    }

    protected processGetUserLinkImage(response: Response): Promise<FileResponse | null> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return response.blob().then(blob => { return { fileName: fileName, data: blob, status: status, headers: _headers }; });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<FileResponse | null>(null as any);
    }
}

export interface IUserLinksClient {

    getUserLinks(): Promise<UserLinkDto[]>;

    followLink(linkId: number): Promise<void>;
}

export class UserLinksClient extends NlpBaseClient implements IUserLinksClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getUserLinks(): Promise<UserLinkDto[]> {
        let url_ = this.baseUrl + "/api/UserLinks";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetUserLinks(_response));
        });
    }

    protected processGetUserLinks(response: Response): Promise<UserLinkDto[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(UserLinkDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserLinkDto[]>(null as any);
    }

    followLink(linkId: number): Promise<void> {
        let url_ = this.baseUrl + "/api/UserLinks/follow/{linkId}";
        if (linkId === undefined || linkId === null)
            throw new Error("The parameter 'linkId' must be defined.");
        url_ = url_.replace("{linkId}", encodeURIComponent("" + linkId));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processFollowLink(_response));
        });
    }

    protected processFollowLink(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }
}

export interface IUserTasksClient {

    getUserTasks(): Promise<UserTaskDto[]>;

    addTask(task: UserTaskDto): Promise<BoolResponse>;

    getTaskCategories(request: BasicSearchRequest): Promise<string[]>;

    getTaskSubCategories(request: BasicSearchRequest): Promise<string[]>;
}

export class UserTasksClient extends NlpBaseClient implements IUserTasksClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getUserTasks(): Promise<UserTaskDto[]> {
        let url_ = this.baseUrl + "/api/UserTasks";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetUserTasks(_response));
        });
    }

    protected processGetUserTasks(response: Response): Promise<UserTaskDto[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(UserTaskDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserTaskDto[]>(null as any);
    }

    addTask(task: UserTaskDto): Promise<BoolResponse> {
        let url_ = this.baseUrl + "/api/UserTasks";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(task);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processAddTask(_response));
        });
    }

    protected processAddTask(response: Response): Promise<BoolResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = BoolResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<BoolResponse>(null as any);
    }

    getTaskCategories(request: BasicSearchRequest): Promise<string[]> {
        let url_ = this.baseUrl + "/api/UserTasks/categories";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetTaskCategories(_response));
        });
    }

    protected processGetTaskCategories(response: Response): Promise<string[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(item);
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<string[]>(null as any);
    }

    getTaskSubCategories(request: BasicSearchRequest): Promise<string[]> {
        let url_ = this.baseUrl + "/api/UserTasks/sub-categories";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetTaskSubCategories(_response));
        });
    }

    protected processGetTaskSubCategories(response: Response): Promise<string[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(item);
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<string[]>(null as any);
    }
}

export class WhoAmIResponse implements IWhoAmIResponse {
    userId!: number;
    name?: string | null;
    email?: string | null;
    signedIn!: boolean;
    claims?: { [key: string]: string; } | null;

    constructor(data?: IWhoAmIResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userId = _data["userId"] !== undefined ? _data["userId"] : <any>null;
            this.name = _data["name"] !== undefined ? _data["name"] : <any>null;
            this.email = _data["email"] !== undefined ? _data["email"] : <any>null;
            this.signedIn = _data["signedIn"] !== undefined ? _data["signedIn"] : <any>null;
            if (_data["claims"]) {
                this.claims = {} as any;
                for (let key in _data["claims"]) {
                    if (_data["claims"].hasOwnProperty(key))
                        (<any>this.claims)![key] = _data["claims"][key] !== undefined ? _data["claims"][key] : <any>null;
                }
            }
            else {
                this.claims = <any>null;
            }
        }
    }

    static fromJS(data: any): WhoAmIResponse {
        data = typeof data === 'object' ? data : {};
        let result = new WhoAmIResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userId"] = this.userId !== undefined ? this.userId : <any>null;
        data["name"] = this.name !== undefined ? this.name : <any>null;
        data["email"] = this.email !== undefined ? this.email : <any>null;
        data["signedIn"] = this.signedIn !== undefined ? this.signedIn : <any>null;
        if (this.claims) {
            data["claims"] = {};
            for (let key in this.claims) {
                if (this.claims.hasOwnProperty(key))
                    (<any>data["claims"])[key] = this.claims[key] !== undefined ? this.claims[key] : <any>null;
            }
        }
        return data;
    }
}

export interface IWhoAmIResponse {
    userId: number;
    name?: string | null;
    email?: string | null;
    signedIn: boolean;
    claims?: { [key: string]: string; } | null;
}

export class SetNewPasswordRequest implements ISetNewPasswordRequest {
    email!: string;
    password!: string;

    constructor(data?: ISetNewPasswordRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"] !== undefined ? _data["email"] : <any>null;
            this.password = _data["password"] !== undefined ? _data["password"] : <any>null;
        }
    }

    static fromJS(data: any): SetNewPasswordRequest {
        data = typeof data === 'object' ? data : {};
        let result = new SetNewPasswordRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email !== undefined ? this.email : <any>null;
        data["password"] = this.password !== undefined ? this.password : <any>null;
        return data;
    }
}

export interface ISetNewPasswordRequest {
    email: string;
    password: string;
}

export class UserLinkDto implements IUserLinkDto {
    linkId!: number;
    userID!: number;
    deleted!: boolean;
    linkOrder!: number;
    followCount!: number;
    dateAddedUtc!: Date;
    dateUpdatedUtc!: Date;
    dateLastFollowedUtc!: Date;
    linkName!: string;
    linkCategory!: string;
    linkUrl!: string;
    linkImage!: string;

    constructor(data?: IUserLinkDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.linkId = _data["linkId"] !== undefined ? _data["linkId"] : <any>null;
            this.userID = _data["userID"] !== undefined ? _data["userID"] : <any>null;
            this.deleted = _data["deleted"] !== undefined ? _data["deleted"] : <any>null;
            this.linkOrder = _data["linkOrder"] !== undefined ? _data["linkOrder"] : <any>null;
            this.followCount = _data["followCount"] !== undefined ? _data["followCount"] : <any>null;
            this.dateAddedUtc = _data["dateAddedUtc"] ? new Date(_data["dateAddedUtc"].toString()) : <any>null;
            this.dateUpdatedUtc = _data["dateUpdatedUtc"] ? new Date(_data["dateUpdatedUtc"].toString()) : <any>null;
            this.dateLastFollowedUtc = _data["dateLastFollowedUtc"] ? new Date(_data["dateLastFollowedUtc"].toString()) : <any>null;
            this.linkName = _data["linkName"] !== undefined ? _data["linkName"] : <any>null;
            this.linkCategory = _data["linkCategory"] !== undefined ? _data["linkCategory"] : <any>null;
            this.linkUrl = _data["linkUrl"] !== undefined ? _data["linkUrl"] : <any>null;
            this.linkImage = _data["linkImage"] !== undefined ? _data["linkImage"] : <any>null;
        }
    }

    static fromJS(data: any): UserLinkDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserLinkDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["linkId"] = this.linkId !== undefined ? this.linkId : <any>null;
        data["userID"] = this.userID !== undefined ? this.userID : <any>null;
        data["deleted"] = this.deleted !== undefined ? this.deleted : <any>null;
        data["linkOrder"] = this.linkOrder !== undefined ? this.linkOrder : <any>null;
        data["followCount"] = this.followCount !== undefined ? this.followCount : <any>null;
        data["dateAddedUtc"] = this.dateAddedUtc ? this.dateAddedUtc.toISOString() : <any>null;
        data["dateUpdatedUtc"] = this.dateUpdatedUtc ? this.dateUpdatedUtc.toISOString() : <any>null;
        data["dateLastFollowedUtc"] = this.dateLastFollowedUtc ? this.dateLastFollowedUtc.toISOString() : <any>null;
        data["linkName"] = this.linkName !== undefined ? this.linkName : <any>null;
        data["linkCategory"] = this.linkCategory !== undefined ? this.linkCategory : <any>null;
        data["linkUrl"] = this.linkUrl !== undefined ? this.linkUrl : <any>null;
        data["linkImage"] = this.linkImage !== undefined ? this.linkImage : <any>null;
        return data;
    }
}

export interface IUserLinkDto {
    linkId: number;
    userID: number;
    deleted: boolean;
    linkOrder: number;
    followCount: number;
    dateAddedUtc: Date;
    dateUpdatedUtc: Date;
    dateLastFollowedUtc: Date;
    linkName: string;
    linkCategory: string;
    linkUrl: string;
    linkImage: string;
}

export class UserTaskDto implements IUserTaskDto {
    taskID!: number;
    userID!: number;
    taskPriority!: number;
    dateAddedUtc!: Date;
    dateCompletedUtc?: Date | null;
    dateDeletedUtc?: Date | null;
    taskName!: string;
    taskCategory!: string;
    taskSubCategory!: string;
    taskDescription!: string;

    constructor(data?: IUserTaskDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.taskID = _data["taskID"] !== undefined ? _data["taskID"] : <any>null;
            this.userID = _data["userID"] !== undefined ? _data["userID"] : <any>null;
            this.taskPriority = _data["taskPriority"] !== undefined ? _data["taskPriority"] : <any>null;
            this.dateAddedUtc = _data["dateAddedUtc"] ? new Date(_data["dateAddedUtc"].toString()) : <any>null;
            this.dateCompletedUtc = _data["dateCompletedUtc"] ? new Date(_data["dateCompletedUtc"].toString()) : <any>null;
            this.dateDeletedUtc = _data["dateDeletedUtc"] ? new Date(_data["dateDeletedUtc"].toString()) : <any>null;
            this.taskName = _data["taskName"] !== undefined ? _data["taskName"] : <any>null;
            this.taskCategory = _data["taskCategory"] !== undefined ? _data["taskCategory"] : <any>null;
            this.taskSubCategory = _data["taskSubCategory"] !== undefined ? _data["taskSubCategory"] : <any>null;
            this.taskDescription = _data["taskDescription"] !== undefined ? _data["taskDescription"] : <any>null;
        }
    }

    static fromJS(data: any): UserTaskDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserTaskDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["taskID"] = this.taskID !== undefined ? this.taskID : <any>null;
        data["userID"] = this.userID !== undefined ? this.userID : <any>null;
        data["taskPriority"] = this.taskPriority !== undefined ? this.taskPriority : <any>null;
        data["dateAddedUtc"] = this.dateAddedUtc ? this.dateAddedUtc.toISOString() : <any>null;
        data["dateCompletedUtc"] = this.dateCompletedUtc ? this.dateCompletedUtc.toISOString() : <any>null;
        data["dateDeletedUtc"] = this.dateDeletedUtc ? this.dateDeletedUtc.toISOString() : <any>null;
        data["taskName"] = this.taskName !== undefined ? this.taskName : <any>null;
        data["taskCategory"] = this.taskCategory !== undefined ? this.taskCategory : <any>null;
        data["taskSubCategory"] = this.taskSubCategory !== undefined ? this.taskSubCategory : <any>null;
        data["taskDescription"] = this.taskDescription !== undefined ? this.taskDescription : <any>null;
        return data;
    }
}

export interface IUserTaskDto {
    taskID: number;
    userID: number;
    taskPriority: number;
    dateAddedUtc: Date;
    dateCompletedUtc?: Date | null;
    dateDeletedUtc?: Date | null;
    taskName: string;
    taskCategory: string;
    taskSubCategory: string;
    taskDescription: string;
}

export class BasicSearchRequest implements IBasicSearchRequest {
    filter?: string | null;
    subFilter?: string | null;

    constructor(data?: IBasicSearchRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.filter = _data["filter"] !== undefined ? _data["filter"] : <any>null;
            this.subFilter = _data["subFilter"] !== undefined ? _data["subFilter"] : <any>null;
        }
    }

    static fromJS(data: any): BasicSearchRequest {
        data = typeof data === 'object' ? data : {};
        let result = new BasicSearchRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["filter"] = this.filter !== undefined ? this.filter : <any>null;
        data["subFilter"] = this.subFilter !== undefined ? this.subFilter : <any>null;
        return data;
    }
}

export interface IBasicSearchRequest {
    filter?: string | null;
    subFilter?: string | null;
}

export class BoolResponse implements IBoolResponse {
    success!: boolean;
    error?: string | null;

    constructor(data?: IBoolResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.success = _data["success"] !== undefined ? _data["success"] : <any>null;
            this.error = _data["error"] !== undefined ? _data["error"] : <any>null;
        }
    }

    static fromJS(data: any): BoolResponse {
        data = typeof data === 'object' ? data : {};
        let result = new BoolResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["success"] = this.success !== undefined ? this.success : <any>null;
        data["error"] = this.error !== undefined ? this.error : <any>null;
        return data;
    }
}

export interface IBoolResponse {
    success: boolean;
    error?: string | null;
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}

export class ApiException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}

(<any>window).NlpBaseClient = NlpBaseClient;