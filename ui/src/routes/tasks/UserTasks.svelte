<style>
	.task {
		background-color: #353535;
		border-radius: 0.5em;
	}
	.task:hover {
		opacity: 0.85;
	}
	.categorization {
		width: 20%;
		font-size: 0.8em;
	}
	.categorization .cat {
		color: #e2ff73;
	}
	.categorization .sub-cat {
		color: #81e4fb;
	}
	.categorization .sub-cat::before {
		content: '/ ';
	}
	.low {
		background-color: #19466b;
	}
	.med {
		background-color: #6b5c19;
	}
	.high {
		background-color: #6b1919;
	}
</style>

<script lang="ts">
	import { Button, Col, Icon, Row } from 'sveltestrap';
	import AddTaskModal from './AddTaskModal.svelte';
	import { UserTasksClient, type UserTaskDto } from '../../nlp-api';
	import { onMount } from 'svelte';
	import { toastError, toastSuccess } from '../../components/ToastManager';
	import EditTaskModal from './EditTaskModal.svelte';

	let tasks: UserTaskDto[] = [];
	let editModal: EditTaskModal;

	const priorityClass = (task: UserTaskDto) => {
		if (task.taskPriority < 128) return 'high';
		if (task.taskPriority < 256) return 'med';
		return 'low';
	};

	const completeTask = async (task: UserTaskDto) => {
		if (!confirm(`Mark "${task.taskName}" as complete?`)) return;
		const response = await new UserTasksClient().completeTask(task.taskID);
		if (response.success) {
			toastSuccess('Success', `Task: "${task.taskName}" has been resolved`);
			refreshTasks();
		} else {
			toastError('Error', response.error || 'Failed to resolve task');
		}
	};

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
			{#each tasks as task}
				<div class="d-flex mb-1 task ps-2 pe-1 py-1 {priorityClass(task)}">
					<div class="flex-fill m-auto">{task.taskName}</div>
					<div class="categorization m-auto">
						<span class="cat">{task.taskCategory}</span>
						<span class="sub-cat">{task.taskSubCategory}</span>
					</div>
					<div class="ms-2">
						<Button on:click={() => editModal.open(task)}>
							<Icon name="pencil-square" />
						</Button>
						<Button color="success" on:click={() => completeTask(task)}>
							<Icon name="clipboard-check" />
						</Button>
					</div>
				</div>
			{/each}
		</div>
	</Col>
</Row>
