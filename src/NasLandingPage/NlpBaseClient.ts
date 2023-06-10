class NlpBaseClient {
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
(<any>window).NlpBaseClient = NlpBaseClient;
