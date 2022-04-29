//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class ConfigClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getClientConfig(): Observable<ClientConfig> {
        let url_ = this.baseUrl + "/api/Config";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetClientConfig(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetClientConfig(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ClientConfig>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ClientConfig>;
        }));
    }

    protected processGetClientConfig(response: HttpResponseBase): Observable<ClientConfig> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ClientConfig.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

@Injectable()
export class ProjectsClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getAll(): Observable<ProjectInfo[]> {
        let url_ = this.baseUrl + "/api/Projects";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ProjectInfo[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ProjectInfo[]>;
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<ProjectInfo[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ProjectInfo.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    syncProject(request: RunCommandRequest): Observable<RunCommandResponse> {
        let url_ = this.baseUrl + "/api/Projects/sync";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSyncProject(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSyncProject(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<RunCommandResponse>;
                }
            } else
                return _observableThrow(response_) as any as Observable<RunCommandResponse>;
        }));
    }

    protected processSyncProject(response: HttpResponseBase): Observable<RunCommandResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = RunCommandResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

@Injectable()
export class UserLinksClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getAll(): Observable<UserLink[]> {
        let url_ = this.baseUrl + "/api/UserLinks";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<UserLink[]>;
                }
            } else
                return _observableThrow(response_) as any as Observable<UserLink[]>;
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<UserLink[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(UserLink.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    getImage(image: string | null): Observable<FileResponse | null> {
        let url_ = this.baseUrl + "/api/UserLinks/image/{image}";
        if (image === undefined || image === null)
            throw new Error("The parameter 'image' must be defined.");
        url_ = url_.replace("{image}", encodeURIComponent("" + image));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/octet-stream"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetImage(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetImage(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<FileResponse | null>;
                }
            } else
                return _observableThrow(response_) as any as Observable<FileResponse | null>;
        }));
    }

    protected processGetImage(response: HttpResponseBase): Observable<FileResponse | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            const fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
            const fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            return _observableOf({ fileName: fileName, data: responseBlob as any, status: status, headers: _headers });
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export class ClientConfig implements IClientConfig {
    columns!: ProjectTableColumn[];
    sonarQubeUrl!: string;
    badges!: string[];

    constructor(data?: IClientConfig) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.columns = [];
            this.badges = [];
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["columns"])) {
                this.columns = [] as any;
                for (let item of _data["columns"])
                    this.columns!.push(item);
            }
            this.sonarQubeUrl = _data["sonarQubeUrl"];
            if (Array.isArray(_data["badges"])) {
                this.badges = [] as any;
                for (let item of _data["badges"])
                    this.badges!.push(item);
            }
        }
    }

    static fromJS(data: any): ClientConfig {
        data = typeof data === 'object' ? data : {};
        let result = new ClientConfig();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.columns)) {
            data["columns"] = [];
            for (let item of this.columns)
                data["columns"].push(item);
        }
        data["sonarQubeUrl"] = this.sonarQubeUrl;
        if (Array.isArray(this.badges)) {
            data["badges"] = [];
            for (let item of this.badges)
                data["badges"].push(item);
        }
        return data;
    }
}

export interface IClientConfig {
    columns: ProjectTableColumn[];
    sonarQubeUrl: string;
    badges: string[];
}

export enum ProjectTableColumn {
    Name = 0,
    RepoType = 1,
    RepoIsPublic = 2,
    SonarQubeBadges = 3,
    HasReadme = 4,
    HasGitAttributes = 5,
    HasPrTemplate = 6,
    HasEditorConfig = 7,
    HasBuildScripts = 8,
    VersionBuildScripts = 9,
    DirSrc = 10,
    DirTests = 11,
    DirDocs = 12,
    DirBuild = 13,
    Languages = 14,
    License = 15,
    Description = 16,
    RepoId = 17,
    RepoDefaultBranch = 18,
    RepoLastUpdate = 19,
    UrlRepo = 20,
    UrlCiCd = 21,
    UrlSonarQube = 22,
    CountRepoForks = 23,
    CountOpenIssues = 24,
    CountBuildScripts = 25,
    CountTestScripts = 26,
    CountWorkFlows = 27,
    RepoSize = 28,
    LastUpdated = 29,
}

export class ProjectInfo implements IProjectInfo {
    name!: string;
    description!: string;
    lastUpdated!: Date;
    metadata!: ProjectInfoMetadata;
    repo!: RepoInfo;
    sonarQube!: SonarQubeInfo;
    scm!: SourceCodeMaturityInfo;
    folders!: ProjectFolderInfo;
    license!: LicenseInfo;
    languages!: string[];

    constructor(data?: IProjectInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.metadata = new ProjectInfoMetadata();
            this.repo = new RepoInfo();
            this.sonarQube = new SonarQubeInfo();
            this.scm = new SourceCodeMaturityInfo();
            this.folders = new ProjectFolderInfo();
            this.license = new LicenseInfo();
            this.languages = [];
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.description = _data["description"];
            this.lastUpdated = _data["lastUpdated"] ? new Date(_data["lastUpdated"].toString()) : <any>undefined;
            this.metadata = _data["metadata"] ? ProjectInfoMetadata.fromJS(_data["metadata"]) : new ProjectInfoMetadata();
            this.repo = _data["repo"] ? RepoInfo.fromJS(_data["repo"]) : new RepoInfo();
            this.sonarQube = _data["sonarQube"] ? SonarQubeInfo.fromJS(_data["sonarQube"]) : new SonarQubeInfo();
            this.scm = _data["scm"] ? SourceCodeMaturityInfo.fromJS(_data["scm"]) : new SourceCodeMaturityInfo();
            this.folders = _data["folders"] ? ProjectFolderInfo.fromJS(_data["folders"]) : new ProjectFolderInfo();
            this.license = _data["license"] ? LicenseInfo.fromJS(_data["license"]) : new LicenseInfo();
            if (Array.isArray(_data["languages"])) {
                this.languages = [] as any;
                for (let item of _data["languages"])
                    this.languages!.push(item);
            }
        }
    }

    static fromJS(data: any): ProjectInfo {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["description"] = this.description;
        data["lastUpdated"] = this.lastUpdated ? this.lastUpdated.toISOString() : <any>undefined;
        data["metadata"] = this.metadata ? this.metadata.toJSON() : <any>undefined;
        data["repo"] = this.repo ? this.repo.toJSON() : <any>undefined;
        data["sonarQube"] = this.sonarQube ? this.sonarQube.toJSON() : <any>undefined;
        data["scm"] = this.scm ? this.scm.toJSON() : <any>undefined;
        data["folders"] = this.folders ? this.folders.toJSON() : <any>undefined;
        data["license"] = this.license ? this.license.toJSON() : <any>undefined;
        if (Array.isArray(this.languages)) {
            data["languages"] = [];
            for (let item of this.languages)
                data["languages"].push(item);
        }
        return data;
    }
}

export interface IProjectInfo {
    name: string;
    description: string;
    lastUpdated: Date;
    metadata: ProjectInfoMetadata;
    repo: RepoInfo;
    sonarQube: SonarQubeInfo;
    scm: SourceCodeMaturityInfo;
    folders: ProjectFolderInfo;
    license: LicenseInfo;
    languages: string[];
}

export class ProjectInfoMetadata implements IProjectInfoMetadata {
    fileName!: string;
    fileNameWithoutExtension!: string;

    constructor(data?: IProjectInfoMetadata) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.fileName = _data["fileName"];
            this.fileNameWithoutExtension = _data["fileNameWithoutExtension"];
        }
    }

    static fromJS(data: any): ProjectInfoMetadata {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectInfoMetadata();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["fileName"] = this.fileName;
        data["fileNameWithoutExtension"] = this.fileNameWithoutExtension;
        return data;
    }
}

export interface IProjectInfoMetadata {
    fileName: string;
    fileNameWithoutExtension: string;
}

export class RepoInfo implements IRepoInfo {
    type!: RepoType;
    url!: string;
    cicd!: string;
    isPublic!: boolean;
    id!: number;
    defaultBranch!: string;
    lastUpdated!: Date;
    forks!: number;
    fullName!: string;
    gitUrl!: string;
    openIssues!: number;
    sshUrl!: string;
    apiUrl!: string;
    size!: number;

    constructor(data?: IRepoInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.url = _data["url"];
            this.cicd = _data["cicd"];
            this.isPublic = _data["isPublic"];
            this.id = _data["id"];
            this.defaultBranch = _data["defaultBranch"];
            this.lastUpdated = _data["lastUpdated"] ? new Date(_data["lastUpdated"].toString()) : <any>undefined;
            this.forks = _data["forks"];
            this.fullName = _data["fullName"];
            this.gitUrl = _data["gitUrl"];
            this.openIssues = _data["openIssues"];
            this.sshUrl = _data["sshUrl"];
            this.apiUrl = _data["apiUrl"];
            this.size = _data["size"];
        }
    }

    static fromJS(data: any): RepoInfo {
        data = typeof data === 'object' ? data : {};
        let result = new RepoInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["url"] = this.url;
        data["cicd"] = this.cicd;
        data["isPublic"] = this.isPublic;
        data["id"] = this.id;
        data["defaultBranch"] = this.defaultBranch;
        data["lastUpdated"] = this.lastUpdated ? this.lastUpdated.toISOString() : <any>undefined;
        data["forks"] = this.forks;
        data["fullName"] = this.fullName;
        data["gitUrl"] = this.gitUrl;
        data["openIssues"] = this.openIssues;
        data["sshUrl"] = this.sshUrl;
        data["apiUrl"] = this.apiUrl;
        data["size"] = this.size;
        return data;
    }
}

export interface IRepoInfo {
    type: RepoType;
    url: string;
    cicd: string;
    isPublic: boolean;
    id: number;
    defaultBranch: string;
    lastUpdated: Date;
    forks: number;
    fullName: string;
    gitUrl: string;
    openIssues: number;
    sshUrl: string;
    apiUrl: string;
    size: number;
}

export enum RepoType {
    Unknown = 0,
    GitHub = 1,
}

export class SonarQubeInfo implements ISonarQubeInfo {
    url!: string;
    id!: string;
    tokenBadge!: string;
    badges!: { [key: string]: string; };

    constructor(data?: ISonarQubeInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.badges = {};
        }
    }

    init(_data?: any) {
        if (_data) {
            this.url = _data["url"];
            this.id = _data["id"];
            this.tokenBadge = _data["tokenBadge"];
            if (_data["badges"]) {
                this.badges = {} as any;
                for (let key in _data["badges"]) {
                    if (_data["badges"].hasOwnProperty(key))
                        (<any>this.badges)![key] = _data["badges"][key];
                }
            }
        }
    }

    static fromJS(data: any): SonarQubeInfo {
        data = typeof data === 'object' ? data : {};
        let result = new SonarQubeInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["url"] = this.url;
        data["id"] = this.id;
        data["tokenBadge"] = this.tokenBadge;
        if (this.badges) {
            data["badges"] = {};
            for (let key in this.badges) {
                if (this.badges.hasOwnProperty(key))
                    (<any>data["badges"])[key] = this.badges[key];
            }
        }
        return data;
    }
}

export interface ISonarQubeInfo {
    url: string;
    id: string;
    tokenBadge: string;
    badges: { [key: string]: string; };
}

export class SourceCodeMaturityInfo implements ISourceCodeMaturityInfo {
    hasReadme!: boolean;
    readme!: string;
    hasGitAttributes!: boolean;
    gitAttributes!: string;
    hasPrTemplate!: boolean;
    hasEditorConfig!: boolean;
    editorConfig!: string;
    hasBuildScripts!: boolean;
    buildScriptVersion!: string;
    buildScripts!: string[];
    testScripts!: string[];
    workFlows!: string[];

    constructor(data?: ISourceCodeMaturityInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.buildScripts = [];
            this.testScripts = [];
            this.workFlows = [];
        }
    }

    init(_data?: any) {
        if (_data) {
            this.hasReadme = _data["hasReadme"];
            this.readme = _data["readme"];
            this.hasGitAttributes = _data["hasGitAttributes"];
            this.gitAttributes = _data["gitAttributes"];
            this.hasPrTemplate = _data["hasPrTemplate"];
            this.hasEditorConfig = _data["hasEditorConfig"];
            this.editorConfig = _data["editorConfig"];
            this.hasBuildScripts = _data["hasBuildScripts"];
            this.buildScriptVersion = _data["buildScriptVersion"];
            if (Array.isArray(_data["buildScripts"])) {
                this.buildScripts = [] as any;
                for (let item of _data["buildScripts"])
                    this.buildScripts!.push(item);
            }
            if (Array.isArray(_data["testScripts"])) {
                this.testScripts = [] as any;
                for (let item of _data["testScripts"])
                    this.testScripts!.push(item);
            }
            if (Array.isArray(_data["workFlows"])) {
                this.workFlows = [] as any;
                for (let item of _data["workFlows"])
                    this.workFlows!.push(item);
            }
        }
    }

    static fromJS(data: any): SourceCodeMaturityInfo {
        data = typeof data === 'object' ? data : {};
        let result = new SourceCodeMaturityInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["hasReadme"] = this.hasReadme;
        data["readme"] = this.readme;
        data["hasGitAttributes"] = this.hasGitAttributes;
        data["gitAttributes"] = this.gitAttributes;
        data["hasPrTemplate"] = this.hasPrTemplate;
        data["hasEditorConfig"] = this.hasEditorConfig;
        data["editorConfig"] = this.editorConfig;
        data["hasBuildScripts"] = this.hasBuildScripts;
        data["buildScriptVersion"] = this.buildScriptVersion;
        if (Array.isArray(this.buildScripts)) {
            data["buildScripts"] = [];
            for (let item of this.buildScripts)
                data["buildScripts"].push(item);
        }
        if (Array.isArray(this.testScripts)) {
            data["testScripts"] = [];
            for (let item of this.testScripts)
                data["testScripts"].push(item);
        }
        if (Array.isArray(this.workFlows)) {
            data["workFlows"] = [];
            for (let item of this.workFlows)
                data["workFlows"].push(item);
        }
        return data;
    }
}

export interface ISourceCodeMaturityInfo {
    hasReadme: boolean;
    readme: string;
    hasGitAttributes: boolean;
    gitAttributes: string;
    hasPrTemplate: boolean;
    hasEditorConfig: boolean;
    editorConfig: string;
    hasBuildScripts: boolean;
    buildScriptVersion: string;
    buildScripts: string[];
    testScripts: string[];
    workFlows: string[];
}

export class ProjectFolderInfo implements IProjectFolderInfo {
    src!: boolean;
    tests!: boolean;
    docs!: boolean;
    build!: boolean;

    constructor(data?: IProjectFolderInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.src = _data["src"];
            this.tests = _data["tests"];
            this.docs = _data["docs"];
            this.build = _data["build"];
        }
    }

    static fromJS(data: any): ProjectFolderInfo {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectFolderInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["src"] = this.src;
        data["tests"] = this.tests;
        data["docs"] = this.docs;
        data["build"] = this.build;
        return data;
    }
}

export interface IProjectFolderInfo {
    src: boolean;
    tests: boolean;
    docs: boolean;
    build: boolean;
}

export class LicenseInfo implements ILicenseInfo {
    name!: string;
    url!: string;

    constructor(data?: ILicenseInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.url = _data["url"];
        }
    }

    static fromJS(data: any): LicenseInfo {
        data = typeof data === 'object' ? data : {};
        let result = new LicenseInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["url"] = this.url;
        return data;
    }
}

export interface ILicenseInfo {
    name: string;
    url: string;
}

export class RunCommandResponse implements IRunCommandResponse {
    command!: string;
    args!: string;
    success!: boolean;
    messages!: string[];
    runningTime!: string;

    constructor(data?: IRunCommandResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.messages = [];
        }
    }

    init(_data?: any) {
        if (_data) {
            this.command = _data["command"];
            this.args = _data["args"];
            this.success = _data["success"];
            if (Array.isArray(_data["messages"])) {
                this.messages = [] as any;
                for (let item of _data["messages"])
                    this.messages!.push(item);
            }
            this.runningTime = _data["runningTime"];
        }
    }

    static fromJS(data: any): RunCommandResponse {
        data = typeof data === 'object' ? data : {};
        let result = new RunCommandResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["command"] = this.command;
        data["args"] = this.args;
        data["success"] = this.success;
        if (Array.isArray(this.messages)) {
            data["messages"] = [];
            for (let item of this.messages)
                data["messages"].push(item);
        }
        data["runningTime"] = this.runningTime;
        return data;
    }
}

export interface IRunCommandResponse {
    command: string;
    args: string;
    success: boolean;
    messages: string[];
    runningTime: string;
}

export class RunCommandRequest implements IRunCommandRequest {
    command!: string;
    args!: string;

    constructor(data?: IRunCommandRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.command = _data["command"];
            this.args = _data["args"];
        }
    }

    static fromJS(data: any): RunCommandRequest {
        data = typeof data === 'object' ? data : {};
        let result = new RunCommandRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["command"] = this.command;
        data["args"] = this.args;
        return data;
    }
}

export interface IRunCommandRequest {
    command: string;
    args: string;
}

export class UserLink implements IUserLink {
    id!: string;
    name!: string;
    url!: string;
    order!: number;
    image!: string;

    constructor(data?: IUserLink) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.url = _data["url"];
            this.order = _data["order"];
            this.image = _data["image"];
        }
    }

    static fromJS(data: any): UserLink {
        data = typeof data === 'object' ? data : {};
        let result = new UserLink();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["url"] = this.url;
        data["order"] = this.order;
        data["image"] = this.image;
        return data;
    }
}

export interface IUserLink {
    id: string;
    name: string;
    url: string;
    order: number;
    image: string;
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

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}