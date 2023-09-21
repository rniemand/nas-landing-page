<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { UserLinksClient } from '../../nlp-api';

	(async () => {
		const links = await new UserLinksClient().getUserLinks();
		console.log('links', links);
	})();
</script>

<Row>
	<Col>User links to be rendered here.</Col>
</Row>
