<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import AddTaskModal from './AddTaskModal.svelte';
</script>

<Row>
	<Col>
		<div>Tasks</div>
		<div class="text-end">
			<AddTaskModal />
		</div>
	</Col>
</Row>
