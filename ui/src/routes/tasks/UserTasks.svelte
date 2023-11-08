<script lang="ts">
	import { Accordion, AccordionItem, Button, Col, Icon, Row } from 'sveltestrap';
	import AddTaskModal from './modals/AddTaskModal.svelte';
	import { UserTasksClient, type UserTaskDto } from '../../nlp-api';
	import { onMount } from 'svelte';
	import { toastError, toastSuccess } from '../../components/ToastManager';
	import EditTaskModal from './modals/EditTaskModal.svelte';
	import TaskInfoDisplay from './components/TaskInfoDisplay.svelte';

	let tasks: UserTaskDto[] = [];
	let editModal: EditTaskModal;

	const onCompleteTask = async (task: UserTaskDto) => {
		if (!confirm(`Mark "${task.taskName}" as complete?`)) return;
		const response = await new UserTasksClient().completeTask(task.taskID);
		if (response.success) {
			toastSuccess('Success', `Task: "${task.taskName}" has been resolved`);
			refreshTasks();
		} else {
			toastError('Error', response.error || 'Failed to resolve task');
		}
	};

	const onEditTask = (task: UserTaskDto) => editModal.open(task);
	const refreshTasks = async () => (tasks = await new UserTasksClient().getUserTasks());

	onMount(() => {
		refreshTasks();
	});
</script>

<Row>
	<Col>
		<div class="text-end">
			<AddTaskModal onTaskAdded={refreshTasks} />
			<EditTaskModal bind:this={editModal} onTaskEdited={refreshTasks} />
		</div>
		<div class="tasks mt-2">
			<Accordion>
				{#each tasks as task}
					<AccordionItem>
						<span class="m-0" slot="header">
							{task.taskName}
						</span>
						<TaskInfoDisplay {task} {onCompleteTask} {onEditTask} />
					</AccordionItem>
				{/each}
			</Accordion>
		</div>
	</Col>
</Row>
