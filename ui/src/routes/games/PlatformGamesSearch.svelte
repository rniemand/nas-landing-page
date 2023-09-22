<script lang="ts">
	import { Button, Col, Icon, Input, Row } from 'sveltestrap';

	export let value: string = '';
</script>

<Row>
	<Col class="d-flex">
		<Input type="text" bind:value placeholder="Filter" class="me-2" />
		<Button color="warning" disabled={value.length === 0} on:click={() => (value = '')}>
			<Icon name="trash" />
		</Button>
	</Col>
</Row>
