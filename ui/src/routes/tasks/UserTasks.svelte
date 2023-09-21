<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import AddTaskModal from './AddTaskModal.svelte';

	const refreshTasks = () => {
		console.log('refreshTasks');
	};
</script>

<Row>
	<Col>
		<div>Tasks</div>
		<div class="text-end">
			<AddTaskModal onTaskAdded={refreshTasks} />
		</div>
	</Col>
</Row>
