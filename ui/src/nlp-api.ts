//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class ImagesClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getImage(path: string): Promise<FileResponse | null> {
        let url_ = this.baseUrl + "/Images/link/{path}";
        if (path === undefined || path === null)
            throw new Error("The parameter 'path' must be defined.");
        url_ = url_.replace("{path}", encodeURIComponent("" + path));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/octet-stream"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetImage(_response);
        });
    }

    protected processGetImage(response: Response): Promise<FileResponse | null> {
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

export class UserLinksClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getAllLinks(): Promise<UserLinkDto[]> {
        let url_ = this.baseUrl + "/UserLinks/all";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetAllLinks(_response);
        });
    }

    protected processGetAllLinks(response: Response): Promise<UserLinkDto[]> {
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

    recordLinkFollow(linkId: number): Promise<boolean> {
        let url_ = this.baseUrl + "/UserLinks/follow/{linkId}";
        if (linkId === undefined || linkId === null)
            throw new Error("The parameter 'linkId' must be defined.");
        url_ = url_.replace("{linkId}", encodeURIComponent("" + linkId));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "PUT",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processRecordLinkFollow(_response);
        });
    }

    protected processRecordLinkFollow(response: Response): Promise<boolean> {
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
        return Promise.resolve<boolean>(null as any);
    }
}

export class UserLinkDto implements IUserLinkDto {
    linkID!: number;
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
            this.linkID = _data["linkID"];
            this.deleted = _data["deleted"];
            this.linkOrder = _data["linkOrder"];
            this.followCount = _data["followCount"];
            this.dateAddedUtc = _data["dateAddedUtc"] ? new Date(_data["dateAddedUtc"].toString()) : <any>undefined;
            this.dateUpdatedUtc = _data["dateUpdatedUtc"] ? new Date(_data["dateUpdatedUtc"].toString()) : <any>undefined;
            this.dateLastFollowedUtc = _data["dateLastFollowedUtc"] ? new Date(_data["dateLastFollowedUtc"].toString()) : <any>undefined;
            this.linkName = _data["linkName"];
            this.linkCategory = _data["linkCategory"];
            this.linkUrl = _data["linkUrl"];
            this.linkImage = _data["linkImage"];
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
        data["linkID"] = this.linkID;
        data["deleted"] = this.deleted;
        data["linkOrder"] = this.linkOrder;
        data["followCount"] = this.followCount;
        data["dateAddedUtc"] = this.dateAddedUtc ? this.dateAddedUtc.toISOString() : <any>undefined;
        data["dateUpdatedUtc"] = this.dateUpdatedUtc ? this.dateUpdatedUtc.toISOString() : <any>undefined;
        data["dateLastFollowedUtc"] = this.dateLastFollowedUtc ? this.dateLastFollowedUtc.toISOString() : <any>undefined;
        data["linkName"] = this.linkName;
        data["linkCategory"] = this.linkCategory;
        data["linkUrl"] = this.linkUrl;
        data["linkImage"] = this.linkImage;
        return data;
    }
}

export interface IUserLinkDto {
    linkID: number;
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