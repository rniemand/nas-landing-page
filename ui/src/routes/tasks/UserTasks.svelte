<script lang="ts">
	import { Col, Row, Table } from 'sveltestrap';
	import AddTaskModal from './AddTaskModal.svelte';
	import { UserTasksClient, type UserTaskDto } from '../../nlp-api';
	import { onMount } from 'svelte';

	let tasks: UserTaskDto[] = [];

	const priorityClass = (task: UserTaskDto) => {
		if (task.taskPriority < 128) return 'table-danger';
		if (task.taskPriority < 256) return 'table-warning';
		return 'table-success';
	};

	const refreshTasks = async () => (tasks = await new UserTasksClient().getUserTasks());

	onMount(() => {
		refreshTasks();
	});
</script>

<Row>
	<Col>
		<div>Tasks</div>
		<div class="text-end">
			<AddTaskModal onTaskAdded={refreshTasks} />
		</div>

		<Table responsive bordered size="sm" striped hover class="mt-2">
			<thead>
				<tr>
					<th>Name</th>
					<th colspan="2">Category</th>
					<th>&nbsp;</th>
				</tr>
			</thead>
			<tbody>
				{#each tasks as task}
					<tr class={priorityClass(task)}>
						<td>{task.taskName}</td>
						<td>{task.taskCategory}</td>
						<td>{task.taskSubCategory}</td>
						<td>Table cell</td>
					</tr>
				{/each}
			</tbody>
		</Table>
	</Col>
</Row>
