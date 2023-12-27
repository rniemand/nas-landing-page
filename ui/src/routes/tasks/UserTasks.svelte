<script lang="ts">
	import { Accordion, AccordionItem, Col, Row } from 'sveltestrap';
	import AddTaskModal from './modals/AddTaskModal.svelte';
	import { UserTasksClient, type UserTaskDto, BasicSearchRequest } from '../../nlp-api';
	import { toastError, toastSuccess } from '../../components/ToastManager';
	import EditTaskModal from './modals/EditTaskModal.svelte';
	import TaskInfoDisplay from './components/TaskInfoDisplay.svelte';
	import TaskFiltering from './components/TaskFiltering.svelte';

	let tasks: UserTaskDto[] = [];
	let editModal: EditTaskModal;
	let category: string | undefined;
	let subCategory: string | undefined;

	const onCompleteTask = async (task: UserTaskDto) => {
		if (!confirm(`Mark "${task.taskName}" as complete?`)) return;
		const response = await new UserTasksClient().completeTask(task.taskID);
		if (response.success) {
			toastSuccess('Success', `Task: "${task.taskName}" has been resolved`);
			refreshTasks(category, subCategory);
		} else {
			toastError('Error', response.error || 'Failed to resolve task');
		}
	};

	const onEditTask = (task: UserTaskDto) => editModal.open(task);

	const refreshTasks = async (cat: string | undefined, subCat: string | undefined) => {
		tasks = [];
		const request = new BasicSearchRequest({
			filter: cat,
			subFilter: subCat
		});
		tasks = await new UserTasksClient().getUserTasks(request);
	};

	$: refreshTasks(category, subCategory);
</script>

<Row>
	<Col>
		<div class="d-flex">
			<TaskFiltering bind:category bind:subCategory allowAllOption />
			<AddTaskModal onTaskAdded={() => refreshTasks(category, subCategory)} />
			<EditTaskModal
				bind:this={editModal}
				onTaskEdited={() => refreshTasks(category, subCategory)} />
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
